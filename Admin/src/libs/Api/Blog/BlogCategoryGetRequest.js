import WebServiceRequest from '../../Api/WebServiceRequest'

class BlogCategoryGetRequest extends WebServiceRequest {
    constructor (context) {
        super(context)
    }
    setId(id){
        super.setTag('BlogCategory/GetById?blogCategoryId='+id)
    }
}
export default BlogCategoryGetRequest;
