import WebServiceRequest from '../../Api/WebServiceRequest'

class BlogCommentDeleteRequest extends WebServiceRequest {
    constructor (context) {
        super(context)
    }
    setId(id){
        super.setTag('BlogComment/DeleteBlogComment?BlogCommentId='+id)
    }
}
export default BlogCommentDeleteRequest;
