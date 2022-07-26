import WebServiceRequest from '../../Api/WebServiceRequest'

class UsersGetAllRequest extends WebServiceRequest {
    constructor (context) {
        super(context)
    }
    setParams(data){
        super.setTag('Users/GetAll?search='+data.search+'&pageNumber='+data.pageNumber+'&count='+data.count);
    }
}
export default UsersGetAllRequest;
