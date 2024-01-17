import WebServiceRequest from '../../Api/WebServiceRequest'

class PostDeleteRequest extends WebServiceRequest {
    constructor (context) {
        super(context)
        super.setTag('Post/Delete')
    }
    setParams(data){
        super.setRequestParamDataObj(data)
    }
}
export default PostDeleteRequest;
