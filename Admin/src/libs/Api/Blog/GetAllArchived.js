import WebServiceRequest from '../../Api/WebServiceRequest'

class BlogGetAllArchived extends WebServiceRequest {
    constructor (context) {
        super(context)
    }
    setParams(data){
        super.setTag(`Blog/GetAllArchived?categoryId=${data.tagId}&pageNumber=${data.pageNumber}&count=${data.count}&searchCommand=${data.search}`)
    }
}
export default BlogGetAllArchived;
