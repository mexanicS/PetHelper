using PetHelper.SharedKernel.ValueObjects;

namespace PetHelper.Core.FileProvider;

public record FileData(Stream Stream, FileInfo FileInfo);

public record FileInfo(FilePath FilePath, string BucketName);