import WebServiceRequest from '../../Api/WebServiceRequest'

class UsersGetRequest extends WebServiceRequest {
    constructor (context) {
        super(context)
    }
    setUserId(id){
        super.setTag('Users/Get?id='+id)
    }
}
export default UsersGetRequest;
