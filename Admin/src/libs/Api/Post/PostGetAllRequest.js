import WebServiceRequest from '../../Api/WebServiceRequest'

class PostGetAllRequest extends WebServiceRequest {
    constructor (context) {
        super(context)
    }
    setParams(data){
        super.setTag('Post/GetAll?pageNumber='+data.pageNumber+'&count='+data.count)
    }
}
export default PostGetAllRequest;
