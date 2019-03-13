(*** hide ***)
#I "../../src/bin/Debug/netstandard1.6"
#I "../../.paket/load/netstandard2.0"
#r "Fable.React.dll"
#r "Fable.Elmish.dll"
#r "Fable.Elmish.React.dll"

(**
A ReactNative app
=======
Let's define our model:
*)

type Model = int

type Msg = Increment | Decrement


(**
### Handle our state initialization and updates
*)

open Elmish

let init () =
  0

let update (msg:Msg) count =
  match msg with
  | Increment -> count + 1
  | Decrement -> count - 1

(**
### Rendering views with ReactNative
Let's open ReactNative bindings and define our view using them:

*)
open Fable.Import.ReactNative
open Fable.Helpers.ReactNative

// define our button element
let button label onPress =
    text [ TextProperties.Style
            [ Color "#FFFFFF"
              TextAlign TextAlignment.Center
              Margin 5.
              FontSize 15. ]]
          label
    |> touchableHighlightWithChild
        [ TouchableHighlightProperties.Style
            [ BackgroundColor "#428bca"
              BorderRadius 4.
              Margin 5. ]
          TouchableHighlightProperties.UnderlayColor "#5499C4"
          OnPress onPress ]

let view count (dispatch:Dispatch<Msg>) =
    let onClick msg =
      fun () -> msg |> dispatch

    // construct RN view
    view [ ViewProperties.Style
            [ AlignSelf Alignment.Stretch
              Padding 20.
              ShadowColor "#000000"
              ShadowOpacity 0.8
              ShadowRadius 3.
              JustifyContent JustifyContent.Center
              Flex 1.
              BackgroundColor "#615A5B" ]]
         [ button "-" (onClick Decrement)
           text [] (string count)
           button "+" (onClick Increment) ]

(**
### Create the program instance
Now we need to connect to native parts generated by react-native CLI using the ID that was used to create it, let's assume we called it `counter`:

*)
open Elmish.ReactNative

Program.mkSimple init update view
|> Program.withReactNative "counter"
|> Program.run

(**
And finally, we have to make changes in default `index.ios.js` and `index.android.js` to call our app:
```js
import {AppRegistry} from 'react-native';
import * as app from './out/App';

```

and that's it.
*)

