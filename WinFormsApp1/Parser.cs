using System.Text.RegularExpressions;

public enum FileReadErrorCode
{
    Success,
    PathIsNull,
    InvalidPath,
    PathTooLong,
    DirectoryNotFound,
    FileNotFound,
    AccessDenied,
    IOError,
    UnsupportedPathFormat,
    UnknownError
}

public struct FileReadResult
{
    public FileReadErrorCode ErrorCode { get; set; }
    public string Message { get; set; }
}

internal class Parser
{
    private string code = string.Empty;
    private string cleanCode = string.Empty;

    public string Code => code;

    public int n1 { get; private set; }
    public int n2 { get; private set; }
    public int N1 { get; private set; }
    public int N2 { get; private set; }

    public int n { get; private set; }
    public int N { get; private set; }
    public double V { get; private set; }

    public FileReadResult ReadCode(string filePath)
    {
        try
        {
            code = File.ReadAllText(filePath);
            cleanCode = Regex.Replace(code, @"//.*|/\*[\s\S]*?\*/", "");

            return new FileReadResult
            {
                ErrorCode = FileReadErrorCode.Success,
                Message = "Файл успешно прочитан."
            };
        }
        catch (Exception ex)
        {
            return new FileReadResult
            {
                ErrorCode = FileReadErrorCode.UnknownError,
                Message = ex.Message
            };
        }
    }


    public Dictionary<string, int> CountOperators()
    {
        var ops = new Dictionary<string, int>();

        string workText = Regex.Replace(
            cleanCode,
            @"""[^""]*""|'[^']+'",
            " "
        );
        int allCommaCount = Regex.Matches(workText, @",").Count;

        workText = Regex.Replace(
            workText,
            @"\bfunc\s+[A-Za-z_]\w*\s*\([^)]*\)(?:\s*[A-Za-z_]\w*)?\s*",
            " "
        );

        int ifElseCount = Regex.Matches(
            workText,
            @"\bif\b(?:(?!\bif\b|\belse\b)[\s\S])*?\belse\b"
        ).Count;

        int allIfCount = Regex.Matches(
            workText,
            @"\bif\b"
        ).Count;

        int allElseCount = Regex.Matches(
            workText,
            @"\belse\b"
        ).Count;

        int standaloneIfCount = allIfCount - ifElseCount;
        int standaloneElseCount = allElseCount - ifElseCount;

        if (ifElseCount > 0)
        {
            ops["if..(else)"] = ifElseCount + standaloneIfCount;
        }

        string[] keywords =
        {
        "for",
        "return",
        "continue",
        "break",
        "switch",
        "case",
        "range"
    };

        foreach (var kw in keywords)
        {
            int count = Regex.Matches(
                workText,
                @"\b" + kw + @"\b"
            ).Count;

            if (count > 0)
            {
                ops[kw] = count;
            }
        }

        var compound = new (string sym, string pattern)[]
        {
        (":=", @":="),
        ("==", @"=="),
        ("!=", @"!="),
        ("<=", @"<="),
        (">=", @">="),
        ("++", @"\+\+"),
        ("--", @"--"),
        ("+=", @"\+="),
        ("-=", @"-="),
        ("*=", @"\*="),
        ("/=", @"/="),
        ("%=", @"%=")
        };

        foreach (var (sym, pattern) in compound)
        {
            int c = Regex.Matches(
                workText,
                pattern
            ).Count;

            if (c > 0)
            {
                ops[sym] = c;

                workText = Regex.Replace(
                    workText,
                    pattern,
                    "  "
                );
            }
        }


        var functionCalls = Regex.Matches(
            workText,
            @"\b(?<name>[A-Za-z_]\w*(?:\.[A-Za-z_]\w*)*)\s*\("
        );

        HashSet<string> systemKeywords = new HashSet<string>
    {
        "if",
        "for",
        "switch"
    };

        int functionCallParenCount = 0;

        foreach (Match match in functionCalls)
        {
            string name = match.Groups["name"].Value;

            if (systemKeywords.Contains(name))
            {
                continue;
            }

            functionCallParenCount++;


            if (name.StartsWith("fmt."))
            {
                string functionName = name + "()";

                ops[functionName] =
                    ops.GetValueOrDefault(functionName, 0) + 1;

                continue;
            }

            if (name.Contains("."))
            {
                string[] parts = name.Split('.');

                for (int i = 0; i < parts.Length - 1; i++)
                {
                    ops["."] =
                        ops.GetValueOrDefault(".", 0) + 1;
                }

                string methodName =
                    parts[parts.Length - 1] + "()";

                ops[methodName] =
                    ops.GetValueOrDefault(methodName, 0) + 1;

                continue;
            }

            string ordinaryFunction =
                name + "()";

            ops[ordinaryFunction] =
                ops.GetValueOrDefault(ordinaryFunction, 0) + 1;
        }


        int openBraces = Regex.Matches(
            workText,
            @"\{"
        ).Count;

        int closeBraces = Regex.Matches(
            workText,
            @"\}"
        ).Count;

        int braces = (openBraces + closeBraces) / 2;

        if (braces > 0)
        {
            ops["{}"] = braces;
        }


        int openBrackets = Regex.Matches(
        workText,
        @"\["
        ).Count;

        int closeBrackets = Regex.Matches(
            workText,
            @"\]"
        ).Count;

        int brackets = (openBrackets + closeBrackets) / 2;

        if (brackets > 0)
        {
            ops["[]"] = brackets;
        }

        int openRound = Regex.Matches(
    workText,
    @"\("
).Count;

        int closeRound = Regex.Matches(
            workText,
            @"\)"
        ).Count;

        int parens = (openRound + closeRound) / 2;

        parens -= functionCallParenCount;

        if (parens > 0)
        {
            ops["()"] = parens;
        }

        var singles = new (string sym, string pattern)[]
        {
            ("=", @"(?<![:!=<>])=(?![=])"),
            ("<", @"<(?![=])"),
            (">", @">(?![=])"),
            ("+", @"\+(?![+=])"),
            ("-", @"-(?![=-])"),
            ("*", @"\*(?![=])"),
            ("/", @"/(?![=])"),
            ("%", @"%(?![=])"),
            ("&&", @"&&"),
            ("||", @"\|\|"),
            ("!", @"!(?![=])"),
            (",", @","),
            (";", @";")
        };

        foreach (var (sym, pattern) in singles)
        {
            int c = Regex.Matches(
                workText,
                pattern
            ).Count;

            if (c > 0)
            {
                ops[sym] = c;
            }
        }

        if (allCommaCount > 0)
        {
            ops[","] = allCommaCount;
        }

        return ops;
    }



    public Dictionary<string, int> CountOperands()
    {
        var operands = new Dictionary<string, int>();

        HashSet<string> skipList = new HashSet<string>
    {
        "package",
        "import",
        "func",
        "var",
        "type",
        "struct",
        "const",
        "if",
        "else",
        "for",
        "return",
        "continue",
        "break",
        "switch",
        "case",
        "range",
        "int",
        "int8",
        "int16",
        "int32",
        "int64",
        "uint",
        "uint8",
        "uint16",
        "uint32",
        "uint64",
        "float32",
        "float64",
        "string",
        "bool",
        "byte",
        "rune",
        "error",
        "any"
    };

        string operandText = Regex.Replace(
            cleanCode,
            @"\bfunc\s+[A-Za-z_]\w*\s*\([^)]*\)(?:\s*[A-Za-z_]\w*)?\s*",
            " "
        );

        operandText = Regex.Replace(
            operandText,
            @"\bpackage\s+[A-Za-z_]\w*",
            " "
        );

        operandText = Regex.Replace(
            operandText,
            @"\b(?<name>[A-Za-z_]\w*(?:\.[A-Za-z_]\w*)*)\s*\(",
            " "
        );

        var matches = Regex.Matches(
            operandText,
            @"\b[a-zA-Z_]\w*\b|\d+|'[^']+'|""[^""]*"""
        );

        foreach (Match m in matches)
        {
            string val = m.Value;

            if (skipList.Contains(val))
            {
                continue;
            }

            operands[val] =
                operands.GetValueOrDefault(val, 0) + 1;
        }

        var functionCalls = Regex.Matches(
            cleanCode,
            @"\b(?<name>[A-Za-z_]\w*(?:\.[A-Za-z_]\w*)*)\s*\("
        );

        foreach (Match match in functionCalls)
        {
            string name = match.Groups["name"].Value;

            if (name == "if" ||
                name == "for" ||
                name == "switch")
            {
                continue;
            }

            string before = cleanCode.Substring(
                0,
                match.Index
            );

            if (Regex.IsMatch(
                before,
                @"\bfunc\s*$"
            ))
            {
                continue;
            }

            if (name.StartsWith("fmt."))
            {
                string functionName =
                    name + "()";

                operands[functionName] =
                    operands.GetValueOrDefault(functionName, 0) + 1;

                continue;
            }

            if (name.Contains("."))
            {
                string[] parts = name.Split('.');

                for (int i = 0; i < parts.Length - 1; i++)
                {
                    if (!skipList.Contains(parts[i]))
                    {
                        operands[parts[i]] =
                            operands.GetValueOrDefault(parts[i], 0) + 1;
                    }
                }

                continue;
            }

            string ordinaryFunction =
                name + "()";

            operands[ordinaryFunction] =
                operands.GetValueOrDefault(ordinaryFunction, 0) + 1;
        }

        return operands;
    }

    public void CalculateHalsteadMetrics(
        Dictionary<string, int> operatorCount,
        Dictionary<string, int> operandCount)
    {
        var activeOps = operatorCount
            .Where(x => x.Value > 0)
            .ToDictionary(x => x.Key, x => x.Value);

        var activeOperands = operandCount
            .Where(x => x.Value > 0)
            .ToDictionary(x => x.Key, x => x.Value);

        n1 = activeOps.Count;
        n2 = activeOperands.Count;

        N1 = activeOps.Values.Sum();
        N2 = activeOperands.Values.Sum();

        n = n1 + n2;
        N = N1 + N2;

        V = n > 0
            ? Math.Round(N * Math.Log2(n), 2)
            : 0;
    }
}
