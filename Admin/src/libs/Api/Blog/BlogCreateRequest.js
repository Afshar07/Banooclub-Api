import WebServiceRequest from '../../Api/WebServiceRequest'

class BlogCreateBlogRequest extends WebServiceRequest {
    constructor (context) {
        super(context)
        super.setTag('Blog/CreateBlog')
    }
    setParams(data){
        super.setRequestParamDataObj(data)
    }
}
export default BlogCreateBlogRequest;
