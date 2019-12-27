순수함수는 주어진 input값에 대한 동작을 수행하여 항상 같은 output을 제공합니다. 이렇듯, 상태를 다루지 않는 특징으로 인해 사이드 이펙트가 없다는 장점이 있습니다. 이러한 일부 순수함수에서 복잡한 계산이 이루어지는 경우가 있습니다.  
예를 들어, 서버 사이드에서 클라이언트에게 이런 값비싼 계산이 포함된 API를 제공해주는 경우, 클라이언트가 1000번 해당 API를 호출했다면 계산 또한 1000번 이루어지게 됩니다. 

이러한 문제점을 해결하기 위하여 우리는 캐시를 사용합니다.

```Kotlin
class FactorGenerator {
    private val factorCache = hashMapOf<Int, List<Int>>()


    private fun isFactor(number: Int, possibleNum: Int) = number % possibleNum == 0


    fun getFactors(number: Int) = factorCache.getOrPut(number, {
        (1..number).toList().filter { curNum -> isFactor(number, curNum) }
    })
}


fun main() {
    val factorGen = FactorGenerator()


    factorGen.getFactors(12) //첫 수행 이후 캐시에 의해 수행 속도가 빨라집니다.
    factorGen.getFactors(12)
    factorGen.getFactors(12)
}
```

해당 getFactor 함수는 첫 수행 이후 cache를 사용하여 더욱 빠른 수행 속도를 보이게 될 것입니다.  
하지만 getFactor 함수는 외부의 cache의 상태에 의존되며 따라서, 순수 함수의 특성을 갖지 않습니다. 따라서 외부 상태에 의존되지 않는 함수 레벨의 캐싱이 필요합니다.

---
# Memoization
메모이제이션이란 자동으로 함수 레벨의 캐싱을 해주는 개념으로써, 순수 함수는 특정 input값을 가공한 output값이 같으므로 이 점을 이용하여 이미 한번 수행된 값을 내부적으로 메모리에 저장하여 수행 속도를 향상시킵니다. (CPU 연산 과정과 메모리 용량의 trade-off)

### 장점
순수함수의 성질을 잃지 않으면서 수행 속도를 높여주고 같은 input값에 대한 재연산을 방지해줍니다.

---
## Example
#### SCALA
```Kotlin
//내부적으로 memoize를 지원하지 않아 확장함수로써 구현하여 사용할 수 있습니다.
fun isFactor(number: Int, possibleNum: Int) = number % possibleNum == 0

fun getFactors(number: Int) = (1..number).toList().filter { curNum -> isFactor(number, curNum) }

//내부적으로 캐싱해주는 클래스
class Memozation<in T, out R>(private val func: (T) -> R): (T) -> R { 
    private val factors = mutableMapOf<T, R>()
    override fun invoke(p1: T): R = factors.getOrPut(p1, { func(p1) })
}

fun <T, R> ((T) -> R).memoize(): (T) -> R = Memozation(this) //function에 대한 memoize 확장 함수 

val memoizeGetFactors = { number: Int -> getFactors(number)}.memoize()

println(memoizeGetFactors(12)) //첫 수행 이후 캐시에 의해 수행 속도가 빨라집니다.
println(memoizeGetFactors(12))
println(memoizeGetFactors(12))
```
#### SCALA
```Scala
// scalaz.memo 패키지를 사용
val isFactor: (Int, Int) => Boolean = (number: Int, potential: Int) => number % potential == 0
val memoizedGetFactors: Int => List[Int] = Memo.immutableHashMapMemo {
  num: Int => (1 to num).toList.filter(curNum => isFactor(num, curNum)) 
}
```

