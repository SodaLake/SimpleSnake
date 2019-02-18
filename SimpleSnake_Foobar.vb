
Class SnakeBody

    Public x As Integer
    Public y As Integer
    Public rotation As Integer
    Public bodyShapeIndex As Integer

    Public Sub New(x As Integer, y As Integer)
        Me.x = x
        Me.y = y
        Me.rotation = 0
        Me.bodyShapeIndex = 12
    End Sub

End Class
Module SimpleSnake_Foobar

    Dim keys(255) As Boolean
    Dim shapes() As Char = {"▲"c, "▶"c, "▼"c, "◀"c, "▽"c, "◁"c, "△"c, "▷"c, "└"c, "┌"c, "┐"c, "┘"c, "│"c, "─"c}
    Dim snake As List(Of SnakeBody)
    Dim directionX As Integer
    Dim directionY As Integer

    Function GetKeyState(key As ConsoleKey)
        Return keys(key)
    End Function

    Sub ProcessInput()
        If directionX = 0 Then
            If GetKeyState(ConsoleKey.A) = True Then
                directionX = -1
                directionY = 0
            ElseIf GetKeyState(ConsoleKey.D) = True Then
                directionX = 1
                directionY = 0
            End If
        ElseIf directionY = 0 Then
            If GetKeyState(ConsoleKey.W) = True Then

                directionY = -1
                directionX = 0
            ElseIf GetKeyState(ConsoleKey.S) = True Then
                directionY = 1
                directionX = 0
            End If
        End If
    End Sub

    Sub Render()
        Console.SetCursorPosition(0, 0)
        Console.WriteLine("간단한 Snake 이동,회전 알고리즘 By Foobar")
        Console.WriteLine("ESC: 프로그램종료")
        Console.SetCursorPosition(snake.First.x * 2, snake.First.y)
        Console.Write(shapes(snake.First.rotation))
        Console.SetCursorPosition(snake.Last.x * 2, snake.Last.y)
        Console.Write(shapes(snake(snake.Count - 2).rotation + 4))
        For i = 1 To snake.Count - 2

            Console.SetCursorPosition(snake(i).x * 2, snake(i).y)
            Console.Write(shapes(snake(i).bodyShapeIndex))
        Next


    End Sub

    Sub Update()
        For i = 0 To 255
            keys(i) = False
        Next
        Do While Console.KeyAvailable = True
            keys(Console.ReadKey(True).Key) = True
        Loop
        ProcessInput()

        Dim tail As SnakeBody = snake.Last
        Dim head As SnakeBody = snake.First


        tail.x = head.x + directionX
        tail.y = head.y + directionY
        tail.rotation = Math.Atan2(directionY, directionX) / Math.PI * 2 + 1


        If tail.rotation <> head.rotation Then
            Dim rotationDifference As Integer = tail.rotation - head.rotation

            If rotationDifference = -1 Or rotationDifference = 3 Then
                head.bodyShapeIndex = ((tail.rotation + 3) Mod 4) + 8

            ElseIf rotationDifference = 1 Or rotationDifference = -3 Then
                head.bodyShapeIndex = tail.rotation + 8
            End If
        Else

            head.bodyShapeIndex = (tail.rotation Mod 2) + 12
        End If



        snake.RemoveAt(snake.Count - 1)
        snake.Insert(0, tail)
    End Sub



    Sub Main()

        directionX = 1
        directionY = 0

        snake = New List(Of SnakeBody)
        snake.Add(New SnakeBody(15, 5))
        snake.Add(New SnakeBody(15, 6))
        snake.Add(New SnakeBody(15, 7))
        snake.Add(New SnakeBody(15, 8))
        snake.Add(New SnakeBody(15, 9))
        snake.Add(New SnakeBody(15, 10))
        snake.Add(New SnakeBody(15, 11))
        snake.Add(New SnakeBody(15, 12))
        snake.Add(New SnakeBody(15, 13))
        snake.Add(New SnakeBody(15, 14))
        snake.Add(New SnakeBody(15, 15))
        snake.Add(New SnakeBody(15, 16))
        snake.Add(New SnakeBody(15, 17))



        Do While GetKeyState(ConsoleKey.Escape) = False
            Render()
            Update()
            Threading.Thread.Sleep(150)
            Console.Clear()
        Loop


    End Sub

End Module
