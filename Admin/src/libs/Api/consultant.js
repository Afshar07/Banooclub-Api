import WebServiceRequest from '../Api/WebServiceRequest'

class ConsultCategoryGetAll extends WebServiceRequest {
    constructor (context) {
        super(context)
        super.setTag('ConsultCategory/GetAll')
    }
}
class ConsultCategoryCreate extends WebServiceRequest {
    constructor (context) {
        super(context)
        super.setTag('ConsultCategory/Create')
    }
    setParams(data){
        super.setRequestParamDataObj(data)
    }
}
class ConsultCategoryUpdate extends WebServiceRequest {
    constructor (context) {
        super(context)
        super.setTag('ConsultCategory/Update')
    }
    setParams(data){
        super.setRequestParamDataObj(data)
    }
}
class ConsultCategoryDelete extends WebServiceRequest {
    constructor (context) {
        super(context)

    }
    setId(id){
        super.setTag(`ConsultCategory/Delete?id=${id}`)
    }
    setParams(data){
        super.setTag()
    }
}
class GetAllConsultantRequests extends WebServiceRequest {
    constructor (context) {
        super(context)
        super.setTag('BecomeConsultantRequest/GetRequests')
    }
    setParams(data){
        super.setRequestParam(data)
    }
}
class GetConsultantRequest extends WebServiceRequest {
    constructor (context) {
        super(context)
        super.setTag('BecomeConsultantRequest/GetById')
    }
    setParams(data){
        super.setRequestParam(data)
    }
}
class AcceptConsultantRequest extends WebServiceRequest {
    constructor (context) {
        super(context)
        super.setTag('BecomeConsultantRequest/AcceptRequest')
    }
    setParams(data){
        super.setRequestParam(data)
    }
}
class RejectConsultantRequest extends WebServiceRequest {
    constructor (context) {
        super(context)
        super.setTag('BecomeConsultantRequest/RejectRequest')
    }
    setParams(data){
        super.setRequestParam(data)
    }
}
class GetAllConsultantComments extends WebServiceRequest {
    constructor (context) {
        super(context)
        super.setTag('ConsultantUserScheduleRating/GetList')
    }
    setParams(data){
        super.setRequestParam(data)
    }
}
class GetCommentDescription extends WebServiceRequest {
    constructor (context) {
        super(context)
        super.setTag('ConsultantUserScheduleRating/Get')
    }
    setParams(data){
        super.setRequestParam(data)
    }
}
class ConfirmConsultantComment extends WebServiceRequest {
    constructor (context) {
        super(context)
        super.setTag('ConsultantUserScheduleRating/Confirm')
    }
    setParams(data){
        super.setRequestParam(data)
    }
}
class DeleteConsultantComment extends WebServiceRequest {
    constructor (context) {
        super(context)
        super.setTag('ConsultantUserScheduleRating/Delete')
    }
    setParams(data){
        super.setRequestParam(data)
    }
}
class GetAllConsultants extends WebServiceRequest {
    constructor (context) {
        super(context)
        super.setTag('ConsultantUserSchedules/GetList')
    }
    setParams(data){
        super.setRequestParam(data)
    }
}
export {
    ConsultCategoryGetAll,
    ConsultCategoryCreate,
    ConsultCategoryUpdate,
    ConsultCategoryDelete,
    GetAllConsultantRequests,
    GetConsultantRequest,
    AcceptConsultantRequest,
    RejectConsultantRequest,
    GetAllConsultantComments,
    GetCommentDescription,
    ConfirmConsultantComment,
    DeleteConsultantComment,
    GetAllConsultants

};
