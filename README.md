# :european_castle: You R Mine <3D RTS-TowerDefense>

 This project was a personal project and it's a humbly imitated game(which is Mushroom Wars) project. 
## Dependencies

+ All code is witten in C#
+ Engine : Unity 2019.4.18f1 (64-bit)  

## Features

>This repository contains : Assets(puchased at Asset store of Unity) , function Scripts
>
>I implemented below functions


1.Towers
  + class Inheritance (Building and kinds of Tower)
  + use Enumsapce.TowerKind to recognize tower kinds and their functions
  + redefinition of abstract function through override to set different stats from child classes
  + simple EnemyTower AI (Get an idea from behaviour tree[composite sequence]) 
  
2.Mouse drag
  + select tower function of departure tower and arrival tower
  + set multiple departure towers to send units same times
  + show upgrade panel and make tower level up
  
3.Unit Move
  +  Use coroutine to give delay of creating and sending units smoothly
  
4.ObjectPool / Singleton Pattern 
  + apply ObjectPool function for optimization with prefabs
  + uses Singleton Patter class for global connection 
  
5.UI
  + move scenes with Async operatione for transition of scene naturally
  + Use UGUI.
  

  
  + [Github](https://github.com/lmharriet/Unity-TowerDefense)

