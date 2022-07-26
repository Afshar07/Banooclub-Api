import WebServiceRequest from '../../Api/WebServiceRequest'

class UserCreateRequest extends WebServiceRequest {
    constructor (context) {
        super(context)
        super.setTag('Users/Create')
    }
    setParams(data){
        super.setRequestParamDataObj(data)
    }
}
export default UserCreateRequest;
