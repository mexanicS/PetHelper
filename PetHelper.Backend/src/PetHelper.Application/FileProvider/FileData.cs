using PetHelper.Domain.ValueObjects;

namespace PetHelper.Application.FileProvider;

public record FileData(Stream Stream, FileInfo FileInfo);

public record FileInfo(FilePath FilePath, string BucketName);