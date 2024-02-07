/*
 TESTS à en faire: (doivent tous renvoyer true)

        string? s;

        s = "ABCDEFGHIJ";
        Console.WriteLine($"{s.GetAsShorten_(0) == string.Empty}");
        Console.WriteLine($"{s.GetAsShorten_(1) == "A"}");
        Console.WriteLine($"{s.GetAsShorten_(2) == "AB"}");
        Console.WriteLine($"{s.GetAsShorten_(3) == "ABC"}");
        Console.WriteLine($"{s.GetAsShorten_(4) == "A..."}");
        Console.WriteLine($"{s.GetAsShorten_(5) == "AB..."}");
        Console.WriteLine($"{s.GetAsShorten_(6) == "ABC..."}");
        Console.WriteLine($"{s.GetAsShorten_(7) == "ABCD..."}");
        Console.WriteLine($"{s.GetAsShorten_(8) == "ABCDE..."}");
        Console.WriteLine($"{s.GetAsShorten_(9) == "ABCDEF..."}");
        Console.WriteLine($"{s.GetAsShorten_(10) == s}");
        Console.WriteLine($"{s.GetAsShorten_(11) == s}");
        Console.WriteLine($"{s.GetAsShorten_(12) == s}");
        Console.WriteLine($"");


        s = "ABCDEFG";
        Console.WriteLine($"{s.GetAsShorten_(0) == string.Empty}");
        Console.WriteLine($"{s.GetAsShorten_(1)=="A"}");
        Console.WriteLine($"{s.GetAsShorten_(2) == "AB"}");
        Console.WriteLine($"{s.GetAsShorten_(3) == "ABC"}");
        Console.WriteLine($"{s.GetAsShorten_(4) == "A..."}");
        Console.WriteLine($"{s.GetAsShorten_(5) == "AB..."}");
        Console.WriteLine($"{s.GetAsShorten_(6) == "ABC..."}");
        Console.WriteLine($"{s.GetAsShorten_(7) == s}");
        Console.WriteLine($"{s.GetAsShorten_(8) == s}");
        Console.WriteLine($"");

        s = "ABCD";
        Console.WriteLine($"{s.GetAsShorten_(0) == string.Empty}");
        Console.WriteLine($"{s.GetAsShorten_(1) == "A"}");
        Console.WriteLine($"{s.GetAsShorten_(2) == "AB"}");
        Console.WriteLine($"{s.GetAsShorten_(3) == "ABC"}");
        Console.WriteLine($"{s.GetAsShorten_(4) == s}");
        Console.WriteLine($"{s.GetAsShorten_(5) == s}");
        Console.WriteLine($"{s.GetAsShorten_(6) == s}");
        Console.WriteLine($"{s.GetAsShorten_(7) == s}");
        Console.WriteLine($"{s.GetAsShorten_(8) == s}");
        Console.WriteLine($"");

        s = "AB";
        Console.WriteLine($"{s.GetAsShorten_(0) == string.Empty}");
        Console.WriteLine($"{s.GetAsShorten_(1) == "A"}");
        Console.WriteLine($"{s.GetAsShorten_(2) == s}");
        Console.WriteLine($"{s.GetAsShorten_(3) == s}");
        Console.WriteLine($"{s.GetAsShorten_(4) == s}");
        Console.WriteLine($"");

        s = "A";
        Console.WriteLine($"{s.GetAsShorten_(0) == string.Empty}");
        Console.WriteLine($"{s.GetAsShorten_(1) == s}");
        Console.WriteLine($"{s.GetAsShorten_(2) == s}");
        Console.WriteLine($"{s.GetAsShorten_(3) == s}");
        Console.WriteLine($"{s.GetAsShorten_(4) == s}");
        Console.WriteLine($"");

        s = "";
        Console.WriteLine($"{s.GetAsShorten_(0) == string.Empty}");
        Console.WriteLine($"{s.GetAsShorten_(1) == s}");
        Console.WriteLine($"{s.GetAsShorten_(2) == s}");
        Console.WriteLine($"{s.GetAsShorten_(3) == s}");
        Console.WriteLine($"{s.GetAsShorten_(4) == s}");
        Console.WriteLine($"");


 */
