open System

// Learn more about F# at https://fsharp.org
// See the 'F# Tutorial' project for more help.

let Random = System.Random()

type Suit = Spade | Dia | Heart | Club 

type Card = {suit:Suit; number:int}

type Concentration = Card * bool * int

let Deck = seq { for s in [Spade;Dia;Heart;Club]  do 
                    for y in 1..12 ->  ({suit=s;number=y},true) }
let Shuffled  = Seq.sortBy (fun x -> Random.Next()) Deck 

let GameDeck = Seq.mapi (fun i (c,b) -> Concentration(c,b,i)) Shuffled
               |> Seq.toList



let printCard (conc:Concentration) = 
    match conc with
        | (_,true,num) -> printf "|→ %3i|" num
        | (card,false,_) ->  match card.suit with
                                | Suit.Spade -> "♠"
                                | Suit.Dia -> "◆"
                                | Suit.Heart -> "♥"
                                | Suit.Club -> "♣"
                             |> printf "|%s "

                             match card.number with
                                 | 11 -> "J"
                                 | 12 -> "Q"
                                 | 13 -> "K"
                                 | (num:int) -> num.ToString()
                             |> printf "%3s|"
       
let pause () = 
    Console.ReadLine() |> ignore

let rec printBoard numberOfOneLine (deck:seq<Concentration>) =
    if Seq.isEmpty deck then
        ()
    else
        let oneline = Seq.take numberOfOneLine deck;
        Seq.iter printCard oneline
        printfn ""
        printBoard numberOfOneLine (Seq.skip numberOfOneLine deck)



type Step = CheckCard | CheckPair

let rec game deck select status count =
    let printGameBoard = printBoard 8
    printGameBoard deck
    printfn "count: %4i" count
    let endGame = Seq.forall (fun (_,x,_) -> not x) deck
    
    if endGame = true then
        ()
    else
        printf "select card:"
        let input = Int32.Parse (Console.ReadLine())
        Console.Clear();

        let card = Seq.find (fun (c:Concentration) -> let (_,_,i) = c 
                                                      if i = input then true else false) deck
        
        let nextDeck = Seq.map (fun x -> if card = x then 
                                            let (c,_,i) = card
                                            (c,false,i)
                                         else
                                            x) deck

        match status with
        | CheckCard -> game nextDeck card CheckPair count
        | CheckPair -> let (c1,_,_) = select 
                       let (c2,_,_) = card
                       if c1.number = c2.number then 
                           game nextDeck card CheckCard (count+1)
                       else
                           printGameBoard nextDeck
                           printfn "Not-Pair"
                           pause ()
                           Console.Clear();
                           let preDeck = Seq.map (fun x -> let (c,_,i) = select
                                                           let (c2,_,i2) = x
                                                           if (c,i) = (c2,i2)  then 
                                                            let (c,_,i) = select
                                                            (c,true,i)
                                                           else 
                                                            x) deck
                           
                           game preDeck card CheckCard (count+1)



[<EntryPoint>]
let main argv =
    printfn "%A" argv

    let dummy = GameDeck.Head

    game GameDeck dummy CheckCard 0
    


    0 // return an integer exit code
