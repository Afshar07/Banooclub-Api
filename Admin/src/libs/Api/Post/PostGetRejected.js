import WebServiceRequest from '../../Api/WebServiceRequest'

class PostGetRejected extends WebServiceRequest {
    constructor (context) {
        super(context)
    }
    setParams(data){
        super.setTag(`PostReport/GetAll?pageNumber=${data.pageNumber}&count=10&search=${data.search}`)
    }
}
export default PostGetRejected;
