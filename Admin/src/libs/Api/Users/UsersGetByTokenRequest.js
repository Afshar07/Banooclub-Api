import WebServiceRequest from '../../Api/WebServiceRequest'

class UsersGeByTokenRequest extends WebServiceRequest {
    constructor (context) {
        super(context)
        super.setTag('Users/GetByToken')
    }

}
export default UsersGeByTokenRequest;
