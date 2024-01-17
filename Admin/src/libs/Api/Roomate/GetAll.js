import WebServiceRequest from '../../Api/WebServiceRequest'
class GetAllRoomate extends WebServiceRequest {
    constructor (context) {
        super(context)
    }
    setParams(data){
        super.setTag(`Roomate/GetAll?mortgageFrom=0&mortgageTo=0`)
    }
}
export default GetAllRoomate;

