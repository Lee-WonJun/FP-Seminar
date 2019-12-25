# Pattern Matching
대상이 특정한 패턴을 가지고 있는가를 확인한다.  
기존의 if문 타입 체크나 switch-case 문의 발전형식이다.  

# Destructuring
구조를 분해하는것. 예를 들어 튜플에서 (a,b) = ("key","value") 면 a와 b에 각각 "key", "value" 가 알아서 할당된다.  
패턴매칭과 같이 사용하여 매칭을 편하게 할 수 있다.  

### 장점
매칭하는 흐름이 쉬워진다.  

### 단점

____
## Example

#### C++
```C++
// Pattern Matching
미지원
// Destructuring
// C++ 17 에 structured binding 기술이 추가되었다.
int array[3] = { 0,1,2 }; 
auto tuple = std::make_tuple(1, 2); 
Struct s = Struct{ 1,2 }; 
auto [x, y, z] = array; 
auto [t1, t2] = tuple; 
auto [s1, s2] = s;
```

#### C#
```C#
// Pattern Matchin 
// C# 7.0 부터 제공된다. 

 if (item is null)   // const pattern
 if (item is 10)  // const pattern
 if (item is Item i) // type pattern // 맞는다면 i 에 자동으로 할당된다. 이때 i는 if문 안 스코프가 아닌 바깥 스코프이다.
 if (item is var x)  // var pattern // var 패턴은 무조건 참이다. 무조건 참인데 쓸데없는 기능같지만 x를 사용하거나, 추루 when 절로 조건 검사를 하기 위함이다.
 
//이때  _ 를 통하여 discarding 할 수 있다.
 
//switch-case문이 확장 되었다.
switch 문에서도 타임매칭이 가능하다.

//MS 공식 예제1.
 switch (shape)
    {
        case Square s when s.Side == 0:     //when 절과 같이 사용
        case Circle c when c.Radius == 0:   //when 절과 같이 사용
            return 0;
        case Square s:
            return s.Side * s.Side;
        case Circle c:
            return c.Radius * c.Radius * Math.PI;
        default:
            ..
    }

//MS 공식 예제2
    string shapeDescription = ~~;
    switch (shapeDescription)
    {
        case "circle":
            return new Circle(2);

        case "square":
            return new Square(4);
        
        case "large-circle":
            return new Circle(12);

        case var o when (o?.Trim().Length ?? 0) == 0:   //var 매칭과 when 절을 같이 사용할수있다.
            // white space
            return null;
        default:
            return "invalid shape description";
    }   
    
// Destructuring
// 튜플은 기본적으로 지원된다.
// 약 3가지 방법으로 가능하다.
// 아래는 그중 하나.
var (name, address, city, _) = GetAddressInfo();
// _ 를 통하여 discarding이 가능하다.

//Deconstruct 함수 오버라이딩을 통한 사용자 지정 Destructuring이 가능하다.

```

#### F#
```F#
//아주 다양한 매칭을 제공한다 

//기본적으로 match - with - | pattern -> 식 꼴이다.

match expression with
| pattern [ when condition ] -> result-expression

// when절을 통하여 추가 조건이 가능하다 (가드
;)

// _를 통한 discarding, 튜플 분해, as로 변환, 변수 대입, head::tail 등등.. 수많은 패턴 매칭이 가능하다.
// 아래는 MS 공식 사이트에서 제공하는 목록이다.

상수 패턴 	  상수 또는 정의 된 리터럴 식별자 	1.0, "test", 30, Color.Red
식별자 패턴 	 구분 된 공용 구조체, 예외 레이블 활성 패턴 사례의 case 값 	Some(x) Failure(msg)
변수 패턴 	  identifier 	a
as 패턴 	   식별자 로 서의 패턴 	(a, b) as tuple1
OR 패턴 	   pattern1 | pattern2 	([h] | [h; _])
및 패턴 	    pattern1 & pattern2 	(a, b) & (_, "test")
단점 패턴 	 identifier :: list 식별자 	h :: t
목록 패턴 	 [ pattern_1; ...; pattern_n ] 	[ a; b; c ]
배열 패턴 	 [| pattern_1; ... pattern_n |] 	[| a; b; c |]
괄호로 묶인 패턴 	( 패턴 ) 	( a )
튜플 패턴 	( pattern_1, ..., pattern_n ) 	( a, b )
레코드 패턴  { identifier1 = pattern_1; ...; identifier_n = pattern_n } 	{ Name = name; }
와일드 카드 패턴 		_
형식 주석과 함께 패턴 	패턴 : 형식 	a : int
형식 테스트 패턴 	:? 유형 [as 식별자 ] 	:? System.DateTime as dt
Null 패턴 	null 	null
```
#### clojure
``` clojure
; 기본적으로 패턴매칭이 제공되지 않는다. cond 로 condition 검사를 한다.
; clojure는 리습 계열이기 때문에 매크로로 쉽게 언어가 확장된다. match 매크로를 확장할수있다.
; clojure의 패턴매칭은 https://github.com/clojure/core.match 으로 확장 가능하다.
; https://github.com/clojure/core.match/wiki/Overview 에서 기능 확인이 가능하다.
```
