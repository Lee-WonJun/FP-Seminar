# Option class
값이 있는지 없는지 여부를 체크하기 위한 타입으로, null이나 error Checking을 안전하게 할 수 있습니다.

### 장점
- null checking을 코드단에서 명시적으로 하여, 좀 더 안정적으로 체크할 수 있습니다.
- 값을 체이닝하여 가져올때 중간에 문제가 생길 수 있는 부분을 안정적으로 처리할 수 있습니다.

____
## Example
#### JAVA
```Java
Map<String, Integer> alphaNumMap = new HashMap<>();
alphaNumMap.put("A", 1);
alphaNumMap.put("B", 1);

Optional<Integer> aValue = Optional.ofNullable(alphaNumMap.get("A"));
Optional<Integer> cValue = Optional.ofNullable(alphaNumMap.get("C"));

System.out.println(aValue.orElseGet(() -> 3)) //값이 없다면 3 출력(같은 자료형이여야 합니다.)

if(cValue.isPresent()) //값이 존재한다면
  System.out.println(cValue.get())
else
  System.out.println("Nothing) //값이 존재하지 않으므로 Nothing 출력됩니다.
```
#### KOTLIN
```Kotlin
val alphaNumMap = mapOf("A" to 1, "B" to 2)

val aValue: Int? = alphaNumMap.get("A") //코틀린에는 Option 클래스 대신 null checking을 해주는 ? 문법이 있습니다.
val cValue: Int? = alphaNumMap.get("C")

println(aValue ?: "Nothing") // ?:(elvis operator)를 사용하여 null일때 대체될 값을 정할 수 있습니다.

cValue?.let { println(it) } ?: println("Nothing") // let 구문을 활용하여 null이 아닐때와 null일때의 처리를 명시적으로 할 수 있습니다.
```

#### SCALA
```Scala
val alphaNumMap = Map("A"-> 1, "B"-> 2)

val aValue: Option[Int] = alphaNumMap.get("A") //값이 있다면 Option의 하위 함수인 Some[T]가 값을 받습니다.
val cValue: Option[Int] = alphaNumMap.get("C") //값이 없다면 Option의 하위 함수인 None이 생성됩니다.

println(aValue.getOrElse("Nothing")) // 1 출력

if(cValue.isDefined) //값이 존재한다면
  println(cValue.get)
else
  println("Nothing") //값이 존재하지 않으므로 Nothing 출력
```

```F#
//option 이 존재한다.
//Option 에는 Some 과 None 이 있다.
let x = Some 10
let result = match x with 
| Some i -> Some(i * i)
| None -> None

```

```clojure
;;동적 타이핑 언어라는점과 OOP 가 아니라 사용하지 않는다.
```
# Either class
Option 클래스에서는 null 여부를 체크할 수 있지만, null일 때 어떤 값을 주입시켜주거나 할 수 없습니다. 하지만 Either에서는 Option과 달리(Null과 다른 값) 하나의 값과 Null일때의 다른 값 2가지 다른 자료형을 갖을 수 있는 값을 다루어 이를 처리할 수 있습니다.

### 장점
- null checking시 null일 때 대신 처리할 값을 처리할 수 있습니다.
- 에러 상황에서 에러와 에러가 나지 않는 상황에서 얻을 수 있는 값을 한번에 처리할 수 있습니다.

____
## Example
#### JAVA
```Java
//Either 클래스와 같은 개념이 존재하지 않습니다.
```
#### KOTLIN
```Kotlin
//Either 클래스와 같은 개념이 존재하지 않습니다.
```

#### SCALA
```Scala
val either: Either[NumberFormatException, Int] = try {
  Right("x".toInt)
} catch {
  case nfe: NumberFormatException => Left(nfe) //에러 상황을 LEFT에 올바른 값을 RIGHT에 담아서 한번에 처리할 수 있습니다.
}
```

