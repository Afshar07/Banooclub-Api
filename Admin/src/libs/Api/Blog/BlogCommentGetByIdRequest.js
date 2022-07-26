import WebServiceRequest from '../../Api/WebServiceRequest'

class BlogCommentGetByIdRequest extends WebServiceRequest {
    constructor (context) {
        super(context)
    }
    setId(id){
        super.setTag('BlogComment/GetById?blogCommentId='+id)
    }
}
export default BlogCommentGetByIdRequest;
