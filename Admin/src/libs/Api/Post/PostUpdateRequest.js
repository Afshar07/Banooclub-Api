import WebServiceRequest from '../../Api/WebServiceRequest'

class PostUpdateRequest extends WebServiceRequest {
    constructor (context) {
        super(context)
        super.setTag('Post/Update')
    }
    setParams(data){
        super.setRequestParamDataObj(data)
    }
}
export default PostUpdateRequest;
