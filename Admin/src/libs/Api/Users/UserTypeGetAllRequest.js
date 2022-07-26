import WebServiceRequest from '../../Api/WebServiceRequest'

class UserTypeGetAllRequest extends WebServiceRequest {
    constructor (context) {
        super(context)
        super.setTag('UserType/GetAll')
    }
}
export default UserTypeGetAllRequest;
