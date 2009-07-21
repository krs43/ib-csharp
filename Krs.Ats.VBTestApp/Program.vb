Imports Krs.Ats.IBNet
Imports Krs.Ats.IBNet.Contracts

Module Program
    ' Fields
    Private client As IBClient
    Private ER2 As Future
    Private Goog As Equity
    Private NextOrderId As Integer

    Sub Main()
        Program.client = New IBClient
        AddHandler Program.client.TickPrice, New EventHandler(Of TickPriceEventArgs)(AddressOf Program.client_TickPrice)
        AddHandler Program.client.TickSize, New EventHandler(Of TickSizeEventArgs)(AddressOf Program.client_TickSize)
        AddHandler Program.client.Error, New EventHandler(Of ErrorEventArgs)(AddressOf Program.client_Error)
        AddHandler Program.client.NextValidId, New EventHandler(Of NextValidIdEventArgs)(AddressOf Program.client_NextValidId)
        Program.client.Connect("127.0.0.1", &H1D48, 10)
        Program.ER2 = New Future("ER2", "GLOBEX", "200803")
        Program.Goog = New Equity("GOOG")
        Program.client.RequestMarketData(12, Program.ER2, Nothing, False, False)
        Program.client.RequestMarketData(13, Program.Goog, Nothing, False, False)
        Do While True
            Threading.Thread.Sleep(100)
        Loop
    End Sub
    Private Sub client_TickSize(ByVal sender As Object, ByVal e As TickSizeEventArgs)
        Console.WriteLine(("Tick Size: " & e.Size))
    End Sub
    Private Sub client_TickPrice(ByVal sender As Object, ByVal e As TickPriceEventArgs)
        Console.WriteLine(("Price: " & e.Price))
    End Sub
    Private Sub client_NextValidId(ByVal sender As Object, ByVal e As NextValidIdEventArgs)
        Console.WriteLine(("Next Valid Id: " & e.OrderId))
        Program.NextOrderId = e.OrderId
    End Sub
    Private Sub client_Error(ByVal sender As Object, ByVal e As ErrorEventArgs)
        Console.WriteLine(("Error: " & e.ErrorMsg))
    End Sub
End Module
