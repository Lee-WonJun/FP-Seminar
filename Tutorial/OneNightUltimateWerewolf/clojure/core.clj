(ns wolf.core
  (:gen-class))
(require '[clojure.core.match :refer [match]])

(def Roll [:Villager :Werewolf :Seer :Robber :Troublemaker :Tanner :Drunk :Hunter :Mason :Insomniac :Minion :Doppelganger])

(defn create-defualt-deck [player]
  (cond
    (= 3 player) [:Werewolf :Werewolf :Seer :Robber :Troublemaker :Villager]
    (= 4 player) [:Werewolf :Werewolf :Seer :Robber :Troublemaker :Villager :Villager]
    (= 5 player) [:Werewolf :Werewolf :Seer :Robber :Troublemaker :Villager :Villager :Villager]))

(def Order (zipmap [:Doppelganger :Werewolf :Minion :Doppelganger :Seer :Robber :Troublemaker :Drunk :Insomniac :Doppelganger] (range) ) )

(defn match-roll [werewolf_fun 
                 seer_fun 
                 robber_fun 
                 troublmake_fun 
                 drunk_fun 
                 insomniac_fun 
                 doppelganger_fun 
                 werewolf 
                 seer 
                 robber
                 troublmaker 
                 drunk 
                 insomniac
                 doppelganger
                 roll]
  (match [roll]
    [:Werewolf] ( werewolf_fun werewolf)
    [:Seer]  (seer_fun seer)
    [:Robber] (robber_fun robber)
    [:Troublemaker]  (troublmake_fun troublmaker)
    [:Drunk]  (drunk_fun drunk)
    [:Insomniac]  (insomniac_fun insomniac)
    [:Doppelganger]  (doppelganger_fun doppelganger)
    :else #()
    )
)

(def discription-of-roll-function
  (repeat 7 println)
  )
(def discription-of-roll-parameter
  ["다른 늑대를 확인하거나 혼자 늑대인 경우 가운대 카드 하나를 확인하세요"
   "가운데 카드 2장을 확인하거나 남의 카드 한장을 확인하세요"
   "남의 카드와 자신의 카드를 변경하시고 자신의 카드를 확인하세요"
   "남의 카드 2개를 변경하세요"
   "Drunk"
   "Doppelganger"
   "자신의 카드를 확인하세요" ]
)

(defn make-match [funtions parameters roll]
  (#(apply match-roll (flatten [funtions parameters roll])))
)
(defn discription-of-roll [roll] 
  (make-match discription-of-roll-function discription-of-roll-parameter roll)
  )


(defn do-roll [player-card dummy-card roll]
  (discription-of-roll roll)
  )

(defn night [player-card dummy-card order]
  (if (empty? order) '(player-card dummy-card)  
      (do (println (first order))
        (let [[player-card dummy-card] (do-roll player-card dummy-card (first order))]
          (recur player-card dummy-card (rest order)))
        )
  )
)


(defn run [player-num]
  (let [deck (->> player-num
                  (create-defualt-deck)
                  (shuffle)
                  )
        order (->> deck
                   (set)
                   (clojure.set/intersection (set (keys Order)))
                   (vec)
                   (sort-by #(get Order %)))
        player-card (take player-num deck)
        dummy-card (drop player-num deck)
        ]
    (night player-card dummy-card order)
  )
)

(defn -main
  []
  (run 3))