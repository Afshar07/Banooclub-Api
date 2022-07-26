import WebServiceRequest from '../../Api/WebServiceRequest'

class UsersChangeUserRoleRequest extends WebServiceRequest {
    constructor (context) {
        super(context)
    }
    setParams(data){
        super.setTag(`Account/ChangeRole?userId=${data.userId}&UserTypeId=${data.typeId}`)
    }
}
export default UsersChangeUserRoleRequest;
