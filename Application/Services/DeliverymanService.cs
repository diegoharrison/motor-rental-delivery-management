using Amazon.S3.Model;
using Amazon.S3;
using MotorRent.DeliveryManagement.Application.Common;
using MotorRent.DeliveryManagement.Application.S3;
using MotorRent.DeliveryManagement.Application.Services.Interfaces;
using MotorRent.DeliveryManagement.Application.Validators;
using MotorRent.DeliveryManagement.Core.Entities;
using MotorRent.DeliveryManagement.Core.Repositories;
using MotorRent.DeliveryManagement.Application.S3.Interfaces;

namespace MotorRent.DeliveryManagement.Application.Services;

public class DeliverymanService : IDeliverymanService
{
    private readonly IDeliverymanRepository _deliverymanRepository;
    private readonly DeliverymanValidator _validator;
    private readonly IS3Service _s3Service; 

    public DeliverymanService(IDeliverymanRepository deliverymanRepository, DeliverymanValidator validator, IS3Service s3Service)
    {
        _deliverymanRepository = deliverymanRepository;
        _validator = validator;
        _s3Service = s3Service;
    }

    public async Task<Deliveryman> GetDeliverymanByIdAsync(int id)
    {        
        return _deliverymanRepository.GetById(id);
    }

    public async Task<Deliveryman> GetDeliverymanByCnpjAsync(string cnpj)
    {
        return _deliverymanRepository.GetByCNPJ(cnpj);
    }

    public async Task<Deliveryman> GetDeliverymanByDriverLicenseNumberAsync(string driverLicenseNumber)
    {
        return _deliverymanRepository.GetByDriverLicenseNumber(driverLicenseNumber);
    }

    public async Task<IEnumerable<Deliveryman>> GetAllDeliverymenAsync()
    {
        return await _deliverymanRepository.GetAllAsync();
    }

    public async Task<OperationResult> CreateOrUpdateDeliveryManAsync(Deliveryman deliveryman)
    {
        try
        {
            var validationResult = await _validator.ValidateAsync(deliveryman);
            if (!validationResult.IsValid)
            {
                var errors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                return new OperationResult { Success = false, Message = $"Validation failed: {errors}" };
            }

            var existingDeliverymanByCnpj = _deliverymanRepository.GetByCNPJ(deliveryman.CNPJ);

            if (existingDeliverymanByCnpj != null)
            {
                return new OperationResult { Success = false, Message = "Já existe um entregador cadastrado com este CNPJ." };
            }

            var existingDeliverymanByDriverLicenseNumber = _deliverymanRepository.GetByDriverLicenseNumber(deliveryman.DriverLicenseNumber);

            if (existingDeliverymanByDriverLicenseNumber != null)
            {
                return new OperationResult { Success = false, Message = "Já existe um entregador cadastrado com este número de CNH." };
            }

            //Aqui está comentado porque não tenho um bucket configurado, tenho conta na aws, mas não cadastrei meu cartão para criar bucket, mas esse trecho de código chama o método UploadImageToS3Async para salvar a imagem, é só descomentar para ver chamando o UploadImageToS3Async que já tem um serviço criado só pro s3.

            //Upload image for S3 bucket
            //string imageUrl = await _s3Service.UploadImageToS3Async(deliveryman.DriverLicenseImage);

            //if (imageUrl == null)
            //{
            //    // Se houve algum erro ao enviar a imagem para o S3, trate-o conforme necessário
            //    Console.WriteLine("Erro ao enviar a imagem para o S3");
            //    return new OperationResult { Success = false, Message = "Erro ao enviar a imagem para o S3." };
            //}

            //deliveryman.DriverLicenseImage = imageUrl;

            var deliverymanObject = new Deliveryman
            {
                Name = deliveryman.Name,
                CNPJ = deliveryman.CNPJ,
                DateOfBirth = deliveryman.DateOfBirth,
                DriverLicenseNumber = deliveryman.DriverLicenseNumber,
                DriverLicenseType = deliveryman.DriverLicenseType,
                DriverLicenseImage = deliveryman.DriverLicenseImage,
            };

            _deliverymanRepository.Add(deliverymanObject);

            return new OperationResult { Success = true, Message = "Entregador adicionado com sucesso." };
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

}
