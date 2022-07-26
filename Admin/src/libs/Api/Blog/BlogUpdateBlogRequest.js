import WebServiceRequest from '../../Api/WebServiceRequest'

class BlogUpdateBlogRequest extends WebServiceRequest {
    constructor (context) {
        super(context)
        super.setTag('Blog/UpdateBlog')
    }
    setParams(data){
        super.setRequestParamDataObj(data)
    }
}
export default BlogUpdateBlogRequest;
