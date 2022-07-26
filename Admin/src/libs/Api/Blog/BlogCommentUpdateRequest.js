import WebServiceRequest from '../../Api/WebServiceRequest'

class BlogCommentUpdateRequest extends WebServiceRequest {
    constructor (context) {
        super(context)
        super.setTag('BlogComment/UpdateBlogComment')
    }
    setParams(data){
        super.setRequestParamDataObj(data)
    }
}
export default BlogCommentUpdateRequest;
