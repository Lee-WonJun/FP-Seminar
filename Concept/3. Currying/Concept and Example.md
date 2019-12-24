# Currying
하나 이상의 파라미터를 받는 함수에서 몇몇 파라미터를 미리 지정하여 새로운 함수로써 사용하는것.  
(본 설명에서는 Currying 과 Partial function 에 차이를 두지않음.)
Currying 이 언어적 차원에서 적용되는경우 fun(1,2,3) 은 fun(1)(2)(3) 처럼 처리된다.

### 장정
중간함수를 작성할 필요가 없어서 편하다.  
함수의 지연실행이 가능
함수합성에서 파라미터를 하나만 받는 함수꼴로 변경


### 단점
솔직히 아직 장점이 체감이 안된다.

____
## Example
C++/C# 은 언어적 차원의 지원이라기보다는 비슷하게 동작하도록 람다 함수를 새로 만든것에 불가함.

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
