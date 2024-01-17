// basic type definition
let id: number = 2
let ids: number[] = [12, 3, 4, 5, 66, 7, 8, 3]
let person: object
person = {
  name: 'saman',
  age: 19,
  IsEmployed: true
}

// unions => Types That Can Have moreThan One Type Specified With |
let IdCard: number | string
IdCard = 10

let Persons: object[] | any[]
Persons = [
  {
    name: 'saman',
    age: 19,
    IsEmployed: true
  },
  {
    name: 'saman',
    age: 19,
    IsEmployed: true
  },
  2, 3, 4, 5, 5, 6, 78, 9
]
// Enums Can Be String Or  Number
enum AdStatus  {
  Active = 1,
  DeActive,
  Pending
}


// Type Assertion
let cId:any = 1
let CustomerId = <string>cId
CustomerId = ''

// Functions => Define Input Types And If Its A return Function Define The OutPut Type ,If NotGive Type Void
function AddNum (x:number,y:number): number {
  return x + y
}
function LogSomeThing (Message:string| any):void{
  console.log(Message)
}


// interfaces => Mostly Used With Objects. You Can Define Each Value of Object Type

interface User {
  name:string,
  Age:string | number,
  IsEmployed?:boolean
}

// Adding Question Mark Makes That Variable NullAble
interface FunctionInterface {
  (x:number| string,y:number) : void
}
let NewUsers : User[]
NewUsers = [
  {
    name: 'saman',
    Age: 19,
  },
  {
    name: 'saman',
    Age: 19,
    IsEmployed: true
  },
]

// Generics => You Can Define A SomeKind Of PlaceHolder For A Function Your Using In
// Multiple Places And Give Function Type In Those Places

function CreateArray<T> (ArrItems:T[]) : T[]{
  return  [].concat(ArrItems)
}


let NumArray = CreateArray<number>([1,2,3,5,67,8,2])
let StringArray = CreateArray<string>(['hello','hola','hey'])


// Type KeyWord => you Can Define Commonly Used types Here To Avoid Duplicating Code. Looks Similar To Interfaces
// But you Use Interfaces MostlyWith Functions Orr Objects

// --------------------------------------- At TsTypes File In Project


// type StrNum = string | number
// let Daii : StrNum = 2
//
//
//
// type StrNum = string | number
//
// interface userInterface {
//   name:StrNum,
//   age:number
// }
//
//
// export {
//   userInterface
// }
