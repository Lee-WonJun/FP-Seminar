// Learn more about F# at http://fsharp.org

open System
open Microsoft.FSharp.Reflection

let Random = System.Random()

type Suit = Spade | Dia | Heart | Club 

type Card = {suit:Suit; number:int}

type Rank = NoPair | OnePair| TwoPair | Tripple | Straight | Flush | FullHouse | FourCard | StraightFlush | RoyalStraight| RoyalStraightFlush

let Deck = seq {for x in [|Spade;Dia;Heart;Club|] do for y in [1..13] do  {suit=x;number=y} } 

let Shuffle = Seq.sortBy (fun x -> Random.Next()) >> Seq.toList
let GameDeck = Shuffle Deck

let Pair (hand:List<Card>) = 
    let counts = Seq.countBy (fun card -> card.number) hand
                |> Seq.map (fun (_,count) -> count)
                |> Seq.sortDescending
                |> Seq.toList

    match counts with
    | 1 :: _ ->  Rank.NoPair
    | 2 :: tail -> match tail with
                   | 1 :: _ -> Rank.OnePair
                   | 2 :: _ -> Rank.TwoPair
                   | _ -> raise (System.Exception("Not Matched!")) 

    | 3 :: tail -> match tail with
                   | 1 :: _ -> Rank.Tripple
                   | 2 :: _ -> Rank.FullHouse
                   | _ -> raise (System.Exception("Not Matched!")) 

    | 4 :: _ -> Rank.FourCard
    | _ -> raise (System.Exception("Not Matched!")) 

let Flush (hand:List<Card>) =
    let counts = Seq.countBy (fun card -> card.suit) hand
                |> Seq.map (fun (_,count) -> count)
                |> Seq.head
    if counts = 5 then Rank.Flush else Rank.NoPair

let Straight (hand:List<Card>) = 
    let numbers = List.map (fun card -> card.number) hand
                  |> List.sort

    if numbers = [1;10;11;12;13] then Rank.RoyalStraight
    else
        let TakeStartToEnd number = 
            let numbers = [1..13]
            let line = numbers @ [1 .. 4]
            line
            |> List.skip (number-1)
            |> List.take 5

        let lineOfNumbers = List.map (fun n -> TakeStartToEnd n) numbers

        let isStraight = List.map (fun line -> line = numbers) lineOfNumbers
                         |> List.reduce (fun b1 b2 -> if b1 || b2 then true else false)

        if isStraight then Rank.Straight else Rank.NoPair


let CheckRank (hand:List<Card>) = 
    let result = [|Pair;Flush;Straight|] |> Array.map (fun f -> f hand)
    
    match result with
    | [|_;Rank.Flush|Rank.RoyalStraight|] -> Rank.RoyalStraightFlush
    | [|_;Rank.Flush|Rank.Straight|] -> Rank.StraightFlush
    | [|Rank.FourCard;_;_|] -> Rank.FourCard
    | [|Rank.FullHouse;_;_|] -> Rank.FullHouse
    | [|_;Rank.Flush;_|] -> Rank.Flush
    | [|_;_;Rank.Straight|] -> Rank.Straight
    | [|Rank.NoPair;Rank.NoPair;Rank.NoPair|] -> NoPair
    | [|x;_;_|] -> x


[<EntryPoint>]
let main argv =
    let hand = List.take 5 GameDeck
    printfn "%A" hand
    printfn "%A" (CheckRank hand)
    0 // return an integer exit code
