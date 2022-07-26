import WebServiceRequest from '../../Api/WebServiceRequest'

class PostsGetNotConfirmed extends WebServiceRequest {
    constructor (context) {
        super(context)
    }
    setParams(data){
        super.setTag(`Post/GetNotConfirmed?pageNumber=${data.pageNumber}&count=10`)
    }
}
export default PostsGetNotConfirmed;
