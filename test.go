package main

import (
	"fmt"
)

func charToValue(c byte) int {
	if c >= '0' && c <= '9' {
		return int(c - '0')
	} else if c >= 'A' && c <= 'Z' {
		return 10 + int(c-'A')
	} else if c >= 'a' && c <= 'z' {
		return 10 + int(c-'a')
	}
	return -1
}

func valueToChar(value int) byte {
	if value >= 0 && value <= 9 {
		return '0' + byte(value)
	} else if value >= 10 && value <= 35 {
		return 'A' + byte(value-10)
	}
	return '0'
}

func validateNumber(number string, base int) bool {
	if len(number) == 0 {
		return false
	}
	for i := 0; i < len(number); i++ {
		val := charToValue(number[i])
		if val == -1 || val >= base {
			return false
		}
	}
	return true
}

func toDecimal(number string, fromBase int) int64 {
	var result int64 = 0
	var power int64 = 1
	for i := len(number) - 1; i >= 0; i-- {
		digit := charToValue(number[i])
		result += int64(digit) * power
		power *= int64(fromBase)
	}
	return result
}

func reverseString(s string) string {
	runes := []rune(s)
	for i, j := 0, len(runes)-1; i < j; i, j = i+1, j-1 {
		runes[i], runes[j] = runes[j], runes[i]
	}
	return string(runes)
}

func fromDecimal(decimalNumber int64, toBase int) string {
	if decimalNumber == 0 {
		return "0"
	}
	var result string
	for decimalNumber > 0 {
		remainder := int(decimalNumber % int64(toBase))
		result += string(valueToChar(remainder))
		decimalNumber /= int64(toBase)
	}
	return reverseString(result)
}

func convertBase(number string, fromBase int, toBase int) string {
	if fromBase < 2 || fromBase > 36 || toBase < 2 || toBase > 36 {
		return "Error: Invalid Base"
	}
	if !validateNumber(number, fromBase) {
		return "Error: Invalid Number"
	}
	if fromBase == toBase {
		return number
	}
	decimal := toDecimal(number, fromBase)
	return fromDecimal(decimal, toBase)
}

func main() {
	number := "1022"
	fromBase := 3
	toBase := 10
	result := convertBase(number, fromBase, toBase)
	fmt.Printf("Input number: %s\n", number)
	fmt.Printf("From Base: %d\n", fromBase)
	fmt.Printf("To Base: %d\n", toBase)
	fmt.Printf("Result string: %s\n", result)
}
