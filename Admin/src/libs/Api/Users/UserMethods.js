import WebServiceRequest from '../../Api/WebServiceRequest'

class UserIndex extends WebServiceRequest {
    constructor (context) {
        super(context)
    }
    setParams(data){
        super.setTag(`Common/GetUserIndexByUserName?userName=${data.userName}`)
    }
}
class GetRoomateByUserId extends WebServiceRequest {
    constructor (context) {
        super(context)
    }
    setParams(data){
        super.setTag(`Roomate/GetByUserId?userId=${data.userId}`)
    }
}
class GetForumByUserId extends WebServiceRequest {
    constructor (context) {
        super(context)
    }
    setParams(data){
        super.setTag(`Forum/GetByUserId?userId=${data.userId}&count=${data.count}&pageNumber=${data.pageNumber}`)
    }
}
class GetOrdersByUserId extends WebServiceRequest {
    constructor (context) {
        super(context)
    }
    setParams(data){
        super.setTag(`Order/GetByUserIdForAdmin?userId=${data.userId}&pageNumber=${data.pageNumber}&count=${data.count}`)
    }
}
class GetPaymentsByUserId extends WebServiceRequest {
    constructor (context) {
        super(context)
    }
    setParams(data){
        super.setTag(`Payment/GetByUserId?userId=${data.userId}&pageNumber=${data.pageNumber}&count=${data.count}`)
    }
}
class GetPostsByUserId extends WebServiceRequest {
    constructor (context) {
        super(context)
    }
    setParams(data){
        super.setTag(`Post/GetByUserId?userId=${data.userId}&pageNumber=${data.pageNumber}&count=${data.count}`)
    }
}
class GetServicesByUserName extends WebServiceRequest {
    constructor (context) {
        super(context)
    }
    setParams(data){
        super.setTag(`ServicePack/GetUserServicesByUserName?lastId=${data.lastId}&count=${data.count}&searchCommand=${data.searchCommand}&userName=${data.userName}`)
    }
}
class GetWishlistByUserId extends WebServiceRequest {
    constructor (context) {
        super(context)
    }
    setParams(data){
        super.setTag(`WishList/GetByUserId?userId=${data.userId}`)
    }
}
class GetAdsByUserId extends WebServiceRequest {
    constructor (context) {
        super(context)
    }
    setParams(data){
        super.setTag(`Ads/GetByUserId?userId=${data.userId}&pageNumber=${data.pageNumber}&count=${data.count}`)
    }
}
export {
    UserIndex,
    GetRoomateByUserId,
    GetForumByUserId,
    GetOrdersByUserId,
    GetPaymentsByUserId,
    GetPostsByUserId,
    GetServicesByUserName,
    GetWishlistByUserId,
    GetAdsByUserId

} ;
