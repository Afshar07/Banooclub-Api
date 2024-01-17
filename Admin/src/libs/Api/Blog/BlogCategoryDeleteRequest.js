import WebServiceRequest from '../../Api/WebServiceRequest'

class BlogCategoryDeleteRequest extends WebServiceRequest {
    constructor (context) {
        super(context)
    }
    setId(id){
        super.setTag('BlogCategory/DeleteBlogCategory?BlogCategoryId='+id)
    }
}
export default BlogCategoryDeleteRequest;
