import WebServiceRequest from '../../Api/WebServiceRequest'

class OrderGetProductsForStudents extends WebServiceRequest {
    constructor (context) {
        super(context)
    }
    setParams(data){
        super.setTag('Order/GetProductsForStudents?searchCommand='+data.searchCommand+'&pageNumber='+data.pageNumber+'&count='+data.count)
    }
}
export default OrderGetProductsForStudents;
