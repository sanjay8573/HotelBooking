select B.Branchid, BookingNumber,BookingDate, CONVERT(VARCHAR(10), B.BookingDate, 103),BS.BookingSourceId,BS.Name as BookingSource,GuestName,CheckIn,Checkout,CommissionPaid from Booking B
inner join BookingSource BS on B.BookingSourceId=BS.BookingSourceId
--WHERE     (CONVERT(VARCHAR(10), B.BookingDate, 103) >= '10/05/2024') and  (CONVERT(VARCHAR(10), B.BookingDate, 103) <= '12/05/2024')
WHERE     BS.Name = 'Yatra' and b.BranchId =3
--WHERE BookingNumber='RMJZZH7P'
;

select * from appLogs order by 1 desc;

exec dbo.sp_getSalesResport "10-05-2024","12-05-2024",null,null,3
exec dbo.sp_getSalesResport null,null,"Yatra",null
exec dbo.sp_getSalesReport @fromDate=null,@toDate=null,@BookingSource=null,@BookingRef='RMJZZH7P',@BranchId=3
exec dbo.sp_getSalesReport @fromDate='',@toDate='',@BookingSource='',@BookingRef='RMJZZH7P',@BranchId=3