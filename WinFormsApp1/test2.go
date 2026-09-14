package main

import "fmt"

func getFactorial(n int) int {
	result := 1
	for i := 1; i <= n; i++ {
		result = result * i
	}
	return result
}

func showMessage() {
	fmt.Println("--- Анализ чисел ---")
}

func main1() {
	showMessage()

	x.append("smth")

	const limit = 10
	var number int = 5

	fact := getFactorial(number)
	fmt.Printf("Факториал числа %d равен %d\n", number, fact)

	if fact > limit && number != 0 {
		fmt.Println("Значение превысило лимит.")
	} else {
		fmt.Println("Значение в пределах нормы.")
	}
}
