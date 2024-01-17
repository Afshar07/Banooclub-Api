import WebServiceRequest from '../../Api/WebServiceRequest'

class PostGetRequest extends WebServiceRequest {
    constructor (context) {
        super(context)
    }
    setId(id){
        super.setTag('Post/Get?postId='+id)
    }
}
export default PostGetRequest;
