import WebServiceRequest from '../../Api/WebServiceRequest'

class UsersAdminSearchRequest extends WebServiceRequest {
    constructor (context) {
        super(context)
    }
    setParams(data){
        super.setTag(`Users/SearchForAdmin?searchCommand=${data.search}&searchType=${data.searchType}`)
    }
}
export default UsersAdminSearchRequest;
