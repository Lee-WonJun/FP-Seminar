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

(defn routine [game-deck]
  (let [x (read-line)
        first (get game-deck (read-string x))]
    (pprint first)
    (let [x (read-line)
          second (get game-deck (read-string x))]
      (pprint second)
      
      ; 카드 번호 같은지 확인.
      )))

(defn -main
  "I don't do a whole lot ... yet."
  [& args]
  (println "Hello, World!"))
