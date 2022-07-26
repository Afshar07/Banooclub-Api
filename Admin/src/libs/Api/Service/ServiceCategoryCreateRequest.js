import WebServiceRequest from '../../Api/WebServiceRequest'

class BlogCategoryCreateRequest extends WebServiceRequest {
    constructor (context) {
        super(context)
        super.setTag('ServiceCategory/Create')
    }
    setParams(data){
        super.setRequestParamDataObj(data)
    }
}
export default BlogCategoryCreateRequest;
