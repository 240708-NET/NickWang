

## Utility Types
Several utility types that "facilitate common type transformations".\
These are basically methods to create a new type with modified properties of an existing type.
- Awaited<Type>: Models await/async functions or the .then() method for Promises
- Partial<Type>: All properties are optional
- Required<Type>: All properties are required (Opposite of Partial)
- Readonly<Type>: All properties are readonly (useful for representing assignment expressions that will fail at runtime)
- Record<Keys, Type>: It's a dictionary
- Record<Type>: Shortcut to defining an object type with a specific
- Pick <Type, Keys>: Type based on another, 'picking' it's properties
- Omit<Type, Keys>
- Exclude<UnionType, ExcludedMembers>
- Extract<Type, Union>
- NonNullable<Type>
- Parameters<Type>
- ConstructorParameters<Type>
- ReturnType<Type>
- InstanceType<Type>: Creates a type with the instance type of constructor
- NoInfer<Type>: Blocks inferences to the contained type
- ThisParameterType<Type> Extracts the type of 'this' parameter for a function
- OmitThisParameter<Type>: Removes the 'this' parameter from type
- ThisType<Type>: Sets the type of the type when refered to as 'this'
[Intrinsic String Manipulation Types]
- Uppercase, Lowercase, Capitalize, Uncapitalize


- Omit<Type, Keys>: removes keys
- Pick<Type, Keys> removes all but specified keys
- Exclude<UnionType, ExcludedMembers>: removes types from a union
- ReturnType<Type>: extract the return type of a function type


## Union Types
- Union types are used when a value can be more than a single type.
- Union Types allow you to define a variable that can hold one of several types. This is useful when a value could be one of multiple types, giving you more flexibility in how you use that value.
- A union type is created using the | (pipe) operator between types.
```js
function foo(value: any){
    if(typeof value === "number"){
        //do thing A
    }else if(typeof value == "string"){
        //do thing B
    }else{
        throw new Error("Only accepts number or string");
    }
}

foo(42); // this will do thing A
foo("test"); // this will do thing B
foo(true); // this will be a syntax error

function boo(value: string | number){
    //do thing C
}

bar(42); // this will do thing C
bar("test"); // this will also do thing C
bar(true); //this will be a syntax error
```
 Union types can also share commom behavior.

```js
interface Bird {
  fly(): void;
  layEggs(): void;
}
 
interface Fish {
  swim(): void;
  layEggs(): void;
}

//this function returns either a Fish or a Bird
declare function getSmallPet(): Fish | Bird;

let pet = getSmallPet();
//layEggs is shared between Fish and Bird and this runs 
pet.layEggs();
//only Fish has swim(), and this is a syntax error
pet.swim();
```
