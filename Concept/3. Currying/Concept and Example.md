# Currying
하나 이상의 파라미터를 받는 함수를 하나의 파라미터를 받는 함수의 조합으로 나타내는것
Currying 이 언어적 차원에서 적용되는경우 fun(1,2,3) 은 fun(1)(2)(3) 처럼 처리된다.

# Partial Application
몇몇 파라미터를 미리 지정하여 새로운 함수로써 사용하는것.

### 장점
중간함수를 작성할 필요가 없어서 편하다.  
함수의 지연실행이 가능
함수합성에서 파라미터를 하나만 받는 함수꼴로 변경
함수 시그니처를 쉽게 맟출 수 있음


### 단점
Currying이 언어적 차원에서 지원되지 않는경우, 한땀한땀 하나의 파라미터를 받는 함수로 만들어주어야함

### Closure / Partial Application
어디까지가 맞는지는 정확하지는 않지만...
Closure 는 문맥을 기억하는 행위, 기술이고
일반적으로 그걸로 만들어진 함수도 Closure 라고 말함

함수를 부분적용 (Partial Application) 하기 위해 부분적으로 문맥을 기억해야하므로 Closure 로 구현되며,
부분적용되어, 문맥을 기억하고 있는 람다함수가 생성되고 그 람다함수도 Closure 하고 명칭함.

____
## Example
C++/C# 은 언어적 차원의 지원이라기보다는 비슷하게 동작하도록 람다 함수 (클로져)를 새로 만든것에 불가함.

#### C++
```C++
bool is_item_in_vector(int x, std::vector<int> list)
{
	for (auto& i : list)
	{
		if (i == x)
		{
			return true;
		}
	}
	return false;
}

int main()
{
	std::vector<int> v = { 1,2,3,4,5 };
	auto is_item_in_v = [v](int x) {return is_item_in_vector(x, v); };

	std::cout << is_item_in_v(1) << std::endl;	//true
	std::cout << is_item_in_v(10) << std::endl;	//false
}
```
#### C#
```C#
static bool IsItemInList(int x, List<int> list)
{
    return list.FindIndex( i => i == x) != -1;
}
static void Main(string[] args)
{
    List<int> seq = new List<int> { 1, 2, 3, 4, 5 };
    Func<int, bool> IsItemInSeq = (int x) => { return IsItemInList(x, seq); };
    Console.WriteLine(IsItemInSeq(1));
    Console.WriteLine(IsItemInSeq(10));
}
```

#### F#
```F#
let IsItemInlist list x = 
    Seq.tryFind (fun i -> i = x) list

[<EntryPoint>]
let main argv =
    let v = [|1;2;3;4;5|]
    let IsItemInV = IsItemInlist v  //real automatic currying
    let IsItemInV2 = (fun i -> IsItemInlist v i)
    printfn "%A" (IsItemInV 5).IsSome
    printfn "%A" (IsItemInV 10).IsSome
    printfn "%A" (IsItemInV2 5).IsSome
    printfn "%A" (IsItemInV2 10).IsSome
    
```
#### clojure
``` clojure
(def seq [1 2 3 4 5])

(defn isItemInList [list x]
  (some  #(= % x) list ))

(defn partial-fun [x]
  ((partial isItemInList seq) x))
```

#### kotlin
``` kotlin
    val listA: List<Int> = listOf(1, 2, 3, 4, 5)
    val IsItemInList: (Int, List<Int>) -> Boolean = { x: Int, list: List<Int> -> list.contains(x) }
    val IsItemInListA: (Int) -> Boolean = { x: Int -> IsItemInList(x, listA) }

    println(IsItemInListA(1)) // true
    println(IsItemInListA(10)) // false
```

#### scala
``` scala
    val listA = List(1, 2, 3, 4, 5)
    val IsItemInList = (x: Int, list: List[Int]) => list.exists(element => element == x)
    val IsItemInListA = (x: Int) => IsItemInList(x, listA)

    println(IsItemInListA(1)) // true
    println(IsItemInListA(10)) // false
```
