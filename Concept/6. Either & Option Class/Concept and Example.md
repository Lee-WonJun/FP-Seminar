# Option class
값이 null이냐 아니냐(있냐 없냐) 여부를 체크하기 위한 타입으로, null checking을 안전하게 할 수 있다.

### 장점
- null checking을 코드단에서 명시적으로 하여, 좀 더 안정적으로 체크할 수 있다.
- 값을 체이닝하여 가져올때 중간에 문제가 생길 수 있는 부분을 안정적으로 처리할 수 있다.

____
## Example
#### JAVA
```Java
Map<String, Integer> alphaNumMap = new HashMap<>();
alphaNumMap.put("A", 1);
alphaNumMap.put("B", 1);
Optional<Integer> aValue = Optional.ofNullable(alphaNumMap.get("A"));
Optional<Integer> cValue = Optional.ofNullable(alphaNumMap.get("C"));

System.out.println(aValue.orElseGet(() -> 3)) //값이 없다면 3 출력(같은 자료형이여야 함)

if(cValue.isPresent()) //값이 존재한다면
  System.out.println(cValue.get())
else
  System.out.println("Nothing) //값이 존재하지 않으므로 Nothing 출력
```
#### KOTLIN
```Kotlin
val alphaNumMap = mapOf("A" to 1, "B" to 2)
val aValue: Int? = alphaNumMap.get("A") //코틀린에는 Option 클래스 대신 null checking을 해주는 ? 문법이 있다.
val cValue: Int? = alphaNumMap.get("C")

println(aValue ?: "Nothing") // ?:(elvis operator)를 사용하여 null일때 대체될 값을 정할 수 있다.

cValue?.let { println(it) } ?: println("Nothing") // let 구문을 활용하여 null이 아닐때와 null일때의 처리를 명시적으로 할 수 있다.
```

#### SCALA
```Scala
val alphaNumMap = Map("A"-> 1, "B"-> 2)
val aValue: Option[Int] = alphaNumMap.get("A") //값이 있다면 Option의 하위 함수인 Some[T]가 값을 받고
val cValue: Option[Int] = alphaNumMap.get("C") //값이 없다면 Option의 하위 함수인 None이 생성된다.

println(aValue.getOrElse("Nothing")) // 1 출력

if(cValue.isDefined) //값이 존재한다면
  println(cValue.get)
else
  println("Nothing") //값이 존재하지 않으므로 Nothing 출력
```

