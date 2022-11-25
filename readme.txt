Planning and designing: Around 2 hour
Coding, debugging and testing: Around 3 hour

Summary of Solution:
-All element in the equation has a space between them which allows to split all elements into an array easily using String.Split(' ') method.
-The input equation is then splited into multiple smaller equation if brackets exist within the equation.
-Each smaller equation is formed by the content within the brackets respectively.
-The execution sequence of the smaller equation is decided by the position of the starting brackets ( .
-The starting brackets located at the later position of the equation is executed first.
-Afterward, if the execution of equation found a smaller equation in it ( which means there is bracket within it), the calculation result of the smaller equation will substitute the smaller equation.
