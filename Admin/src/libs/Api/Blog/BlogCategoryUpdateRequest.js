

import WebServiceRequest from '../../Api/WebServiceRequest'

class BlogCategoryUpdateRequest extends WebServiceRequest {
    constructor (context) {
        super(context)
        super.setTag('BlogCategory/UpdateBlogCategory');
    }
    setData(data){
        super.setRequestParamDataObj(data)
    }
}
export default BlogCategoryUpdateRequest;


