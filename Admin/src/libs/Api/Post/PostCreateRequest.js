import WebServiceRequest from '../../Api/WebServiceRequest'

class PostCreateRequest extends WebServiceRequest {
    constructor (context) {
        super(context)
        super.setTag('Post/Create')
    }
    setParams(data){
        super.setRequestParamDataObj(data)
    }
}
export default PostCreateRequest;
