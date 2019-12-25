# Lazy
필요(요구)할떄 결과를 계산하는 방식.  
필요할때 객체를 생성하면 Lazy Initialization  
필요할때 식을 평가하면 Lazy Evaluation  
 

### 장점
처음에 모든것을 초기화하는데 장시간 소요되는것으르 막음.  
무한한 리스트를 생성할수있다.  

### 단점
필요할때 마다 계산을 요한다. (단 자주쓰이는 경우 메모지에이션이 가능하다).  
지연과 즉시중에 좋은 경우를 골라 잘 사용해야함.  

____
## Example
C++의 경우 Lazy 리스트를 지원하지는 않으나 클래스 객체를 만들때 연산자 오버로딩등을 통하여 지연되게 생성 시킬 수 있다.

#### C++
```C++
// 전체 코드 생략
```
#### C#
```C#
// IEnumerable 과 yield 키워드를 통하여 Lazy 하게 구현할수있다.
// Lazy Init 을 위해 Lazy<T> 클래스를 제공한다.
```

#### F#
```F#
// Lazy 키워드를 제공한다.
let x = 10
let result = lazy (x + 10)
printfn "%d" (result.Force()) //Force 는 한번만 실행된다.
//이는 C# 의 Lazy<T> 클래스로 구현된다.

// Sequence (seq) 를 제공한다.
// Seq는 c# 에서 IEnumberable 에 해당한다.
// 무한 시퀀스도 만들수 있다.
let seqInfinite =
    Seq.initInfinite (fun index ->
        let n = float (index + 1)
        1.0 / (n * n * (if ((index + 1) % 2 = 0) then 1.0 else -1.0)))

printfn "%A" seqInfinite
    
```
#### clojure
``` clojure
;lazy 함수가 존재 (lazy-seq)
;Clojure는 완전히 lazy한 것이 아니며 map, reduce, filter 또는 반복과 같은 대부분의 시퀀스 작업 만 lazy 하다.
;clojure 의 range는 lazy 한 무한 시퀀스이다.
;dorun 과 doall 등의 키워드를 통하여 realize 할수있다 (F#의 포스와 비슷)

;map   filter   remove   range   take   take-while   drop   drop-while
;repeat    iterate    cycle 등..

```
