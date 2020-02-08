module GameEngine

open System

type Roll =
    |Villager        // 마을주민(Villager) 3장
    |Werewolf        // 늑대인간(Werewolf) 2장
    |Seer            // 예언자(Seer) 1장
    |Robber          // 강도(Robber) 1장
    |Troublemaker    // 말썽쟁이(Troublemaker) 1장
    |Tanner          // 무두장이(Tanner) 1장
    |Drunk           // 주정뱅이(Drunk) 1장
    |Hunter          // 사냥꾼(Hunter) 1장
    |Mason           // 프리메이슨(Mason) 2장
    |Insomniac       // 불면증환자(Insomniac) 1장
    |Minion          // 하수인(Minion) 1장
    |Doppelganger    // 도플갱어(Doppelganger) 1장


//3인플: 늑대인간 2명, 예언자 1명, 도둑 1명, 문제아 1명, 마을주민 1명
//4인플: 3인플 구성에 마을주민 1명 추가
//5인플: 3인플 구성에 마을주민 2명 추가

//ORDER : 도플갱어 - 늑대인간 - 하수인 - 하수인/도플갱어 - 비밀요원 - 예언자 - 강도 - 말썽쟁이 - 주정뱅이 - 불면증환자 - 도플갱어/불면증환자

let Order = [|Doppelganger;Werewolf;Minion;Doppelganger;Seer;Robber;Troublemaker;Drunk;Insomniac;Doppelganger|]
let CreateDefaultDeck (player : int) =
    match player with
    | 3 -> Some ([|Werewolf;Werewolf;Seer;Robber;Troublemaker;Villager|])
    | 4 -> Some ([|Werewolf;Werewolf;Seer;Robber;Troublemaker;Villager;Villager|] )
    | 5 -> Some ([|Werewolf;Werewolf;Seer;Robber;Troublemaker;Villager;Villager;Villager|] )
    | _ -> None 


let AddPlayer (list: List<Roll>) (card : Roll) =
    list @ [card]

let ChangeCard ((list: Roll[]) ,(index1 : int), (index2 : int) )=
    (Seq.mapi (fun idx v -> 
    match idx with
    |_ when idx =index1 -> list.[index2]
    |_ when idx = index2 -> list.[index1]
    |_ -> v
    )  list)

let CheckCard ((list:Roll[]) , (index1 : int)) =
    list.[index1]

let GetSelfIndexOrDupliate ((list:Roll[]), (roll : Roll)) =
    let index1 = Seq.tryFindIndex (fun x -> x = roll) list
    let index2 = Seq.tryFindIndexBack (fun x -> x = roll) list

    if index1.IsSome && index1 <> index2 then
        None
    else
        Some(index1.Value)

let CheckSameTeam ((list:Roll[]) , (roll :Roll)) =
    let index1 = Seq.tryFindIndex (fun x -> x = roll) list
    let index2 = Seq.tryFindIndexBack (fun x -> x = roll) list
    if index1.IsSome && index1 <> index2 then
        Some ((index1.Value,index2.Value))
    else
        None


let MatchRoll (werewolf_fun , seer_fun, robber_fun, troublmake_fun, drunk_fun, insomniac_fun, doppelganger_fun) 
    (werewolf , seer, robber, troublmaker, drunk, insomniac, doppelganger) (roll:Roll)  =
    match roll with
    | Werewolf -> werewolf_fun werewolf
    | Seer -> seer_fun seer
    | Robber -> robber_fun robber
    | Troublemaker -> troublmake_fun troublmaker
    | Drunk -> drunk_fun drunk
    | Insomniac -> insomniac_fun insomniac
    | Doppelganger -> doppelganger_fun doppelganger
    | _ -> raise (System.Exception("Not Matched"))



let PrintString s = printfn "%s" s
let DescriptOfRollFunction = (PrintString, PrintString, PrintString, PrintString, PrintString, PrintString, PrintString )
let DescriptOfRollParameter = ( "다른 늑대를 확인하거나 혼자 늑대인 경우 가운대 카드 하나를 확인하세요" ,
                                "가운데 카드 2장을 확인하거나 남의 카드 한장을 확인하세요",
                                "남의 카드와 자신의 카드를 변경하시고 자신의 카드를 확인하세요" ,
                                "남의 카드 2개를 변경하세요" ,
                                "Drunk" ,
                                "자신의 카드를 확인하세요" ,
                                "Doppelganger" )


let DescriptionOfRoll = (MatchRoll DescriptOfRollFunction  DescriptOfRollParameter ) 

let WerewolfTurn (player, dummy, roll, turn)  = 
    let isSameTeam  = CheckSameTeam (player, roll)
    
    if isSameTeam.IsNone then
        printfn "Only you, Check Dummy :"
        let input = Int32.Parse (Console.ReadLine())
        printfn "%A" (CheckCard (dummy, input))

    else
        printfn "%A" isSameTeam.Value

    (player, dummy)

let SeerTurn (player, dummy, roll, turn)  = 
    printfn "You're Seer, 1. Check two card in dummy cards 2.Check a card in player cards :"
    let input = Int32.Parse (Console.ReadLine())
    printfn "Check card:"
    let mutable index = Int32.Parse (Console.ReadLine())

    match input with
    | 1 -> printfn "%A" (CheckCard (dummy, index))
           printfn "Check another card:"
           index <- Int32.Parse (Console.ReadLine())
           printfn "%A" (CheckCard (dummy, index))

    | 2 -> printfn "%A" (CheckCard (player, index))
    | _ -> raise (System.Exception("Out of Commads"))

    (player, dummy)

let RobberTurn (player, dummy, roll, turn)  = 
    printfn "You're Robber, Choose another player card:"
    let input = Int32.Parse (Console.ReadLine())

    
    printfn "%A" (CheckCard (player, input))
    let changedPlayer = ChangeCard (player, turn, input)

    (Seq.toArray changedPlayer,dummy)

let TroublemakerTurn (player, dummy, roll, turn)  = 
    printfn "You're Troublemaker, Choose player cards:"

    let index1 = Int32.Parse (Console.ReadLine())
    printfn "Choose another card:"
    let index2 = Int32.Parse (Console.ReadLine())

    if index1 = turn|| index2 = turn then
        raise (System.Exception("you cannot change your card"))
    else
        let changedPlayer = ChangeCard (player, index1, index2)
        (Seq.toArray changedPlayer,dummy)

let NotImplemntateTurn (player, dummy, roll, turn)  = 
    (player, dummy)

let InsomniacTurn (player, dummy, roll, turn)  = 
    printf "You're Insomniac, Your roll is"
    printfn "%A" (CheckCard (player, turn))
    (player, dummy)

let TurnOfRollFunction = (WerewolfTurn, SeerTurn, RobberTurn, TroublemakerTurn, NotImplemntateTurn, InsomniacTurn, NotImplemntateTurn )
    
let TurnOfRoll = (MatchRoll TurnOfRollFunction) 

let DoRoll (player: Roll[]) (dummy: Roll[]) (roll : Roll) (turn: Option<int>)  : Roll[] * Roll[] =
    DescriptionOfRoll roll

    if (Array.tryFind (fun x -> x = roll) player).IsSome then
        let PlayerTuple = (player, dummy,  roll, if turn.IsSome then turn.Value else -1)
        TurnOfRoll (PlayerTuple, PlayerTuple, PlayerTuple, PlayerTuple, PlayerTuple, PlayerTuple, PlayerTuple) roll
    else
        (player,dummy)
    




let Night (player: Roll[]) (dummy: Roll[]) (order : Roll[]) =
    let rec Night_Rec (firstPlayer: Roll[]) (player: Roll[]) (dummy: Roll[]) (order : Roll[]) =
       if Array.isEmpty order then
           (player, dummy)
       else
           let turn = order.[0]
           printfn "Turn : %A" turn
           let updatedPlayer,updatedDummy = DoRoll player dummy turn (Array.tryFindIndex (fun x -> x = turn) firstPlayer)
           Night_Rec firstPlayer updatedPlayer updatedDummy (Array.skip 1 order)
    
    Night_Rec player player dummy order




let Run (playerNumber : int) =
    let rand = new System.Random();

    let createdDeck = CreateDefaultDeck playerNumber

    let suffledDeck = 
        match createdDeck with 
        | None -> None
        | Some x -> Some(Array.sortBy (fun _-> rand.Next()) createdDeck.Value) 

    printfn "Suffle:%A" suffledDeck

    let playerCard = Array.truncate playerNumber suffledDeck.Value
    let dummyCard = Array.skip playerNumber suffledDeck.Value
    printfn "Player:%A" playerCard
    printfn "Dummy:%A" dummyCard



    let playOrder = Set.intersect (Set.ofArray Order) (Set.ofArray suffledDeck.Value) |> Set.toArray
    

    printfn "%A" playOrder

    let GameEnd = Night playerCard dummyCard playOrder

    printfn "Player:%A" (fst GameEnd)
    printfn "Dummy:%A" (snd GameEnd)

    

    

