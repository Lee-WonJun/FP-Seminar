(ns concentration-game.core
  (:require [clojure.pprint :refer :all])
  (:gen-class))

(def suits [:Spade :Dia :Heart :Club])


(defrecord Card [suit number])
(defrecord Concentration [is-open card index] )


(def Deck (for [suit suits number (range 1 13)] (Card. suit number) ))
(def Shuffled (vec (shuffle Deck)))

(def GameDeck
  (vec (map-indexed #(Concentration. false %2 %1) Shuffled)))

(defn print-deck [game-deck]
  (let [printed-info
        (map #(if (true? (:is-open %)) {:card (:card %) :index (:index %)} {:card "Closed" :index (:index %)}   ) game-deck)
        ]
    (pprint printed-info)))

(defn get-number  [concentration] (-> concentration
                                      (:card)
                                      (:number)))

(defn open-card-in-deck [concentration deck]
  (mapv #(if (= concentration %)
           (assoc % :is-open true)
           %) deck))

(defn routine [game-deck]

  (let [x (read-line)
        first  (get game-deck (read-string x))
        first-number (get-number first)
        _ (pprint first)
        y (read-line)
        second (get game-deck (read-string y))
        second-number (get-number second)
        _ (pprint second)]
    (if (= first-number second-number) 
      (->> game-deck
           (open-card-in-deck first)
           (open-card-in-deck second)       
           )
      
      game-deck)
      ; 카드 번호 같은지 확인.
    ))

(defn clear? [game-deck]
  (every? #(:is-open %) game-deck))

(defn loop-game [game-deck]
  (print-deck game-deck)
  
  (if (clear? game-deck)
    (println "clear")
    (recur (routine game-deck))))


(defn -main
  "I don't do a whole lot ... yet."
  [& args]
  (loop-game GameDeck))
