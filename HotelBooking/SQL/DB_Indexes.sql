CREATE  INDEX booking_branchId ON booking(branchid) ;
CREATE  INDEX bookedRoom_branchId ON bookedRoom(branchid) ;
CREATE  INDEX staffmember_branchId ON staffmember(branchid) ;
CREATE  INDEX staffLogin_StaffId ON staffLogin(StaffId) ;
CREATE  INDEX amenities_branchId ON amenities(branchid) ;
CREATE  INDEX Room_Type_branchId ON Room_Type(branchid) ;
CREATE  INDEX PriceManager_branchId ON PriceManager(branchid) ;
CREATE  INDEX PriceManager_RoomTypeId ON PriceManager(RoomTypeId) ;
CREATE  INDEX PriceManager_BranchId_RoomTypeId ON PriceManager(RoomTypeId,branchid) ;


select * from bookedRoom;
select * from staffmember;
select * from staffLogin;
select * from amenities;
select * from Room_Type;
select * from PriceManager;