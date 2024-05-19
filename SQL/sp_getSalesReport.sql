alter PROCEDURE sp_getSalesReport(@fromDate varchar(10), @toDate varchar(10),@BookingSourceid  int,@bookingRef  varchar(20),@BranchId int)
AS
BEGIN
IF @fromDate !='' and  @toDate!='' 
	select BookingNumber as BookingRef,BookingDate, BS.BookingSourceId,BS.Name as BookingSource,GuestName,CheckIn as CheckInDate,Checkout CheckOutDate,TotalAmount as BookingAmount,CommissionPaid from Booking B
	inner join BookingSource BS on B.BookingSourceId=BS.BookingSourceId
	WHERE     (CONVERT(VARCHAR(10), B.BookingDate, 103) >= @fromDate) and  (CONVERT(VARCHAR(10), B.BookingDate, 103) <= @toDate) and b.BranchId =@BranchId

ELSE IF @fromDate !='' and  @toDate!=''  and @bookingRef !=''
	select BookingNumber as BookingRef,BookingDate, BS.BookingSourceId,BS.Name as BookingSource,GuestName,CheckIn as CheckInDate,Checkout CheckOutDate,TotalAmount as BookingAmount,CommissionPaid from Booking B
	inner join BookingSource BS on B.BookingSourceId=BS.BookingSourceId
	WHERE (CONVERT(VARCHAR(10), B.BookingDate, 103) >= @fromDate) and  (CONVERT(VARCHAR(10), B.BookingDate, 103) <= @toDate) and b.BranchId =@BranchId
	and BookingNumber=@bookingRef
ELSE IF @BookingSourceid >0
	select BookingNumber as BookingRef,BookingDate, BS.BookingSourceId,BS.Name as BookingSource,GuestName,CheckIn as CheckInDate,Checkout CheckOutDate,TotalAmount as BookingAmount,CommissionPaid from Booking B
	inner join BookingSource BS on B.BookingSourceId=BS.BookingSourceId and b.BranchId =@BranchId
	WHERE     BS.BookingSourceId = @BookingSourceid
ElSE

	select BookingNumber as BookingRef,BookingDate, BS.BookingSourceId,BS.Name as BookingSource,GuestName,CheckIn as CheckInDate,Checkout CheckOutDate,TotalAmount as BookingAmount,CommissionPaid from Booking B
	inner join BookingSource BS on B.BookingSourceId=BS.BookingSourceId and b.BranchId =@BranchId
	WHERE BookingNumber=@bookingRef
END;