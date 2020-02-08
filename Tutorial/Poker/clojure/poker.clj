(ns poker.core
  (:gen-class))

(require '[clojure.core.match :refer [match]])

(defrecord Card [suit number])

(def suits [:Spade :Dia :Heart :Club])

(def Deck (for [suit suits number (range 1 13)] (Card. suit number)))

(def GameDeck (vec (shuffle Deck)))

(def rank [:NoPair
           :OnePair
           :TwoPair
           :Tripple
           :Straight
           :Flush
           :FullHouse
           :FourCard
           :StraightFlush
           :RoyalStraight
           :RoyalStraightFlush])

(defn pair [hand]
  (let [numbers (->> hand
                     (map #(.number %))
                     frequencies
                     (sort-by #(val %) >)
                     (map val))]
    (match [numbers]
      [([1 & r] :seq)] :NoPair
      [([2 1 & r] :seq)] :OnePair
      [([2 2 & r] :seq)] :TwoPair
      [([3 1 & r] :seq)] :Tripple
      [([3 2 & r] :seq)] :FullHouse
      [([4 & r] :seq)] :FourCard)))

(defn flush [hand]
  (let [counts (->> hand
                    (map #(.suit %))
                    frequencies
                    first
                    val)]
    (if (= 5 counts) :Flush :NoPair)))

(defn straight [hand]
  (let [sort-hand-number  (->> hand
                               (map #(.number %))
                               sort)
        set-hand-number (set sort-hand-number)]
    (if (= #{1 10 11 12 13} set-hand-number)
      :RoyalStraight
      (let [in-seq (concat (range 1 14) (range 1 5))
            availble-straight (->> in-seq
                                   (partition 5 1)
                                   (map set))
            some-hand (some #{set-hand-number} availble-straight)]
        (if some-hand :Straight :NoPair)))))

(defn check-rank [hand]
  (let [results ((juxt pair flush straight) hand)]
    (match results
      [_ :Flush :RoyalStraight] :RoyalStraightFlush
      [_ :Flush :Straight] :StraightFlush
      [:FourCard _ _]  :FourCard
      [:FullHouse _ _] :FullHouse
      [_  :Flush _]  :Flush
      [_ _ :Straight] :Straight
      [:NoPair :NoPair :NoPair]  :NoPair
      [x _ _]  x)))

(defn -main
  "I don't do a whole lot ... yet."
  [& args]
  (println "Hello, World!"))
